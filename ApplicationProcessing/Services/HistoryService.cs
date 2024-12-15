using Database.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationProcessing.Services
{
    internal class HistoryService
    {
        /// <summary>
        /// <para>
        ///     Compare passed <see cref="Application"/> with same one in database with same Ids
        /// </para>
        /// <para>
        ///     Call this method after <see cref="DbContext.SaveChanges()"/>
        /// </para>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<bool> Add(Application application, Operation.Operations operation)
        {
            if (application is null)
                throw new ArgumentNullException(nameof(application));

            try
            {
                using (var context = new Database.Core.Context())
                {
                    string description = "";

                    switch (operation)
                    {
                        case Operation.Operations.Create:
                            description = "Application created";
                            break;

                        case Operation.Operations.Update:
                            Application oldApplication = await context.Applications.AsNoTracking().Include(item => item.Tags).FirstAsync(item => item.Id.Equals(application.Id));

                            List<string> changes = new List<string>();

                            if (!application.Title.Equals(oldApplication.Title))
                                changes.Add($"{nameof(application.Title)} changed");

                            if (!application.Content.Equals(oldApplication.Content))
                                changes.Add($"{nameof(application.Content)} changed");

                            if (!application.ClientId.Equals(oldApplication.ClientId))
                                changes.Add($"{nameof(application.Client)} changed");

                            if (!application.OperatorId.Equals(oldApplication.OperatorId))
                                changes.Add($"{nameof(application.Operator)} changed");

                            if (!application.StatusId.Equals(oldApplication.StatusId))
                                changes.Add($"{nameof(application.Status)} changed");

                            if (application.Tags.Count != oldApplication.Tags.Count)
                                changes.Add($"{nameof(application.Tags)} changed");
                            else
                            {
                                var oldTags = oldApplication.Tags.Select(item => item.Id);
                                var tags = application.Tags.Select(item => item.Id);

                                for (int i = 0; i < tags.Count(); i++)
                                    if (oldTags.ElementAt(i) != tags.ElementAt(i))
                                    {
                                        changes.Add($"{nameof(application.Tags)} changed");
                                        break;
                                    }
                            }

                            description = "Application changed:\n" + string.Join("\n", changes);
                            break;

                        case Operation.Operations.Delete:
                            description = "Application deleted";
                            break;
                    }

                    History history = new History
                    {
                        Timestamp = application.Updated,
                        Description = description,
                        ApplicationId = application.Id,
                        OperatorId = AuthorizationService.User.Id,
                        OperationId = (long)operation
                    };

                    await context.History.AddAsync(history);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}