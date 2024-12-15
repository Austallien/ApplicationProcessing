using Database.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Core.Utils
{
    public class TestDataProvider
    {
        public TestDataProvider()
        {
            ProvidePersonsWithUserdata();
            ProvideApplicationsWithHistory();
        }

        private void ProvidePersonsWithUserdata()
        {
            using (var context = new Context())
            {
                if (context.Persons.FirstOrDefault(item => item.FirstName.Equals("Кудряшов")) is null)
                    context.Persons.Add(new Models.Person
                    {
                        FirstName = "Кудряшов",
                        MiddleName = "Роман",
                        LastName = "Родионович",
                        Birth = DateTime.Parse("16.07.1996"),
                        RoleId = (long)Role.Roles.Client,
                        User = new Models.Userdata
                        {
                            Username = "Client_0",
                            Login = "client_0",
                            Password = "password"
                        }
                    });

                if (context.Persons.FirstOrDefault(item => item.FirstName.Equals("Волков")) is null)
                    context.Persons.Add(new Models.Person
                    {
                        FirstName = "Волков",
                        MiddleName = "Алексей",
                        LastName = "Ильич",
                        Birth = DateTime.Parse("08.03.2001"),
                        RoleId = (long)Role.Roles.Client,
                        User = new Models.Userdata
                        {
                            Username = "Client_1",
                            Login = "client_1",
                            Password = "password"
                        }
                    });

                if (context.Persons.FirstOrDefault(item => item.FirstName.Equals("Морозов")) is null)
                    context.Persons.Add(new Models.Person
                    {
                        FirstName = "Морозов",
                        MiddleName = "Даниил",
                        LastName = "Степанович",
                        Birth = DateTime.Parse("01.03.1987"),
                        RoleId = (long)Role.Roles.Operator,
                        User = new Models.Userdata
                        {
                            Username = "Operator_0",
                            Login = "operator_0",
                            Password = "password"
                        }
                    });

                if (context.Persons.FirstOrDefault(item => item.FirstName.Equals("Ильин")) is null)
                    context.Persons.Add(new Models.Person
                    {
                        FirstName = "Ильин",
                        MiddleName = "Артём",
                        LastName = "Маркович",
                        Birth = DateTime.Parse("27.11.1979"),
                        RoleId = (long)Role.Roles.Operator,
                        User = new Models.Userdata
                        {
                            Username = "Operator_1",
                            Login = "operator_1",
                            Password = "password"
                        }
                    });

                context.SaveChanges();
            }
        }

        private void ProvideApplicationsWithHistory()
        {
            using (var context = new Context())
            {
                DateTime time = DateTime.UtcNow;

                if (context.Applications.FirstOrDefault(item => item.Title.Equals("Нет звука приятнее, чем шёпот бессменных лидеров")) is null)
                    context.Applications.Add(new Application
                    {
                        Created = time,
                        Updated = time,
                        Title = "Нет звука приятнее, чем шёпот бессменных лидеров",
                        Content = "Для современного мира современная методология разработки не оставляет шанса для поставленных обществом задач. Следует отметить, что разбавленное изрядной долей эмпатии, рациональное мышление влечет за собой процесс внедрения и модернизации прогресса профессионального сообщества. В своём стремлении повысить качество жизни, они забывают, что разбавленное изрядной долей эмпатии, рациональное мышление прекрасно подходит для реализации форм воздействия.",
                        ClientId = 1,
                        OperatorId = null,
                        StatusId = (long)Status.Statuses.New,
                        Tags = new List<Tag>(),
                        History = new List<History> { new History
                        {
                            Timestamp = time,
                            Description = "Application has been created",
                            OperatorId = 3,
                            OperationId = (long)Operation.Operations.Create
                        }
                    }
                    });

                time = DateTime.UtcNow;

                if (context.Applications.FirstOrDefault(item => item.Title.Equals("Мелочь, а приятно: объемы выросли")) is null)
                    context.Applications.Add(new Application
                    {
                        Created = time,
                        Updated = time,
                        Title = "Мелочь, а приятно: объемы выросли",
                        Content = "Банальные, но неопровержимые выводы, а также стремящиеся вытеснить традиционное производство, нанотехнологии, вне зависимости от их уровня, должны быть указаны как претенденты на роль ключевых факторов. Но элементы политического процесса неоднозначны и будут призваны к ответу! В частности, дальнейшее развитие различных форм деятельности не даёт нам иного выбора, кроме определения поэтапного и последовательного развития общества.",
                        ClientId = 1,
                        OperatorId = null,
                        StatusId = (long)Status.Statuses.New,
                        Tags = new List<Tag>(),
                        History = new List<History> { new History
                        {
                            Timestamp = time,
                            Description = "Application has been created",
                            OperatorId = 3,
                            OperationId = (long)Operation.Operations.Create
                        }
                    }
                    });

                time = DateTime.UtcNow;

                if (context.Applications.FirstOrDefault(item => item.Title.Equals("Может показаться странным, но объемы выросли")) is null)
                    context.Applications.Add(new Application
                    {
                        Created = time,
                        Updated = time,
                        Title = "Может показаться странным, но объемы выросли",
                        Content = "Прежде всего, синтетическое тестирование требует анализа переосмысления внешнеэкономических политик. Господа, постоянный количественный рост и сфера нашей активности является качественно новой ступенью дальнейших направлений развития. Господа, современная методология разработки влечет за собой процесс внедрения и модернизации стандартных подходов.",
                        ClientId = 2,
                        OperatorId = null,
                        StatusId = (long)Status.Statuses.New,
                        Tags = new List<Tag>(),
                        History = new List<History> { new History
                        {
                            Timestamp = time,
                            Description = "Application has been created",
                            OperatorId = 4,
                            OperationId = (long)Operation.Operations.Create
                        }
                    }
                    });

                context.SaveChanges();
            }
        }
    }
}