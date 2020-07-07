using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YappiesTesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YappiesTesting.Data
{
    public static class YTSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new yappiesTestingContext(
                serviceProvider.GetRequiredService<DbContextOptions<yappiesTestingContext>>()))
            {
                //Program Supervisors
                if (!context.ProgramSupervisors.Any())
                {
                    context.ProgramSupervisors.AddRange(
                    new ProgramSupervisor
                    {
                        FirstName = "Michael",
                        LastName = "Morris",
                        Email = "mmorris@yahoo.com",
                        Phone = 2895039392
                    },

                    new ProgramSupervisor
                    {
                        FirstName = "Rick",
                        LastName = "Rossy",
                        Email = "rickyrossy@gmail.com",
                        Phone = 9053849384
                    },

                    new ProgramSupervisor
                    {
                        FirstName = "Stephen",
                        LastName = "Johnson",
                        Email = "sjohnson@hotmail.com",
                        Phone = 2899119111
                    },

                    new ProgramSupervisor
                    {
                        FirstName = "Eric",
                        LastName = "Ribeye",
                        Email = "speaker22@outlook.com",
                        Phone = 2896669898
                    },

                    new ProgramSupervisor
                    {
                        FirstName = "Felix",
                        LastName = "Medil",
                        Email = "fmedil@gmail.com",
                        Phone = 2894903929
                    },

                    new ProgramSupervisor
                    {
                        FirstName = "Adam",
                        LastName = "Singer",
                        Email = "pianolover@hotmail.com",
                        Phone = 9059492999
                    },

                    new ProgramSupervisor
                    {
                        FirstName = "Ken",
                        LastName = "Dall",
                        Email = "kdall1@hotmail.com",
                        Phone = 2899054929
                    },

                    new ProgramSupervisor
                    {
                        FirstName = "Dave",
                        LastName = "Borris",
                        Email = "dborris@gmail.com",
                        Phone = 9042942999
                    });
                    context.SaveChanges();
                }
                //Programs
                if (!context.Programs.Any())
                {
                    context.Programs.AddRange(
                    new Models.Program
                    {
                        ProgramName = "Adult Lane Swims",
                        ProgramDescription = "Participants may pay daily or purchase a pass. Enjoy a relaxing swim or work on strokes during this adult only pool time. (1-hour swim)",
                        ProgramJoinCode = "AAAAAAAAAA",
                        ProgramSupervisorID = context.ProgramSupervisors.FirstOrDefault(p => p.LastName == "Morris").ID
                    },
                    new Models.Program
                    {
                        ProgramName = "Swimmer 1-6",
                        ProgramDescription = "Our swimmer program makes sure children learn how to swim before they get in too deep. Swimmer progressions accommodate 5-12 years old children and youth including absolute beginners as well as swimmers who want to build on the basics. We stress lots of in-water practice to develop solid swimming strokes and skills. We incorporate Water Smart education at all levels. (45-minute lesson)",
                        ProgramJoinCode = "BBBBBBBBBB",
                        ProgramSupervisorID = context.ProgramSupervisors.FirstOrDefault(p => p.LastName == "Rossy").ID
                    },
                    new Models.Program
                    {
                        ProgramName = "Ball Hockey",
                        ProgramDescription = "They shoot, they score! What a way to learn Canada's favourite past time. Your child will develop coordination, teamwork and game strategy while making friends and having fun. Skills are enhanced through drills and games.",
                        ProgramJoinCode = "CCCCCCCCCC",
                        ProgramSupervisorID = context.ProgramSupervisors.FirstOrDefault(p => p.LastName == "Johnson").ID
                    },
                    new Models.Program
                    {
                        ProgramName = "Basketball",
                        ProgramDescription = "Come and dribble your cares away with this fun introduction to an exciting sport. Skills of the game are taught and reinforced through drills and practices. Slam dunk!",
                        ProgramJoinCode = "DDDDDDDDDD",
                        ProgramSupervisorID = context.ProgramSupervisors.FirstOrDefault(p => p.LastName == "Ribeye").ID
                    },
                    new Models.Program
                    {
                        ProgramName = "Gymnastics 9-12 yrs",
                        ProgramDescription = "The recreational gymnastics classes are designed to teach children the basics of the sport with emphasis on skill building and fun. Certified instructors will be working with the children on coo-ordination, balance and flexibility while keeping safety in mind. children will participate in the following disciplines, floor, low beam, mini trampoline and vault.",
                        ProgramJoinCode = "EEEEEEEEEE",
                        ProgramSupervisorID = context.ProgramSupervisors.FirstOrDefault(p => p.LastName == "Medil").ID
                    },
                    new Models.Program
                    {
                        ProgramName = "Fort Erie Teen Zone",
                        ProgramDescription = "The Teen Zone is open for youth ages 13-19 years. Many activities are planned by youth themselves. Youth can access computers and assistance with resume writing or job searching, open gym time, games room, computer labs and out trips. Themed nights like a coffee house, and movie nights are organized throughout the year. Fort Erie Centre - 20 Lewis Street Tuesdays and Thursdays 7:00 PM-9:00 PM",
                        ProgramJoinCode = "FFFFFFFFFF",
                        ProgramSupervisorID = context.ProgramSupervisors.FirstOrDefault(p => p.LastName == "Singer").ID
                    },
                    new Models.Program
                    {
                        ProgramName = "JR Dance",
                        ProgramDescription = "This program is an introduction to the world of dance for your child. The children will explore the fundamentals of ballet in a safe and nurturing environment. This program focuses on movement, co-ordination and spatial awareness. The emphasis of the program is on having fun.",
                        ProgramJoinCode = "GGGGGGGGGG",
                        ProgramSupervisorID = context.ProgramSupervisors.FirstOrDefault(p => p.LastName == "Dall").ID
                    },
                    new Models.Program
                    {
                        ProgramName = "Bronze Star",
                        ProgramDescription = "Bronze Star is the pre-bronze medallion training standard and excellent preparation for success in Bronze Medallion. Participants develop problem solving and decision making skills as individuals and in partners. Participants will learn CPR and develop Water Smart, confidence and the lifesaving skills needed to be a lifeguard.",
                        ProgramJoinCode = "HHHHHHHHHH",
                        ProgramSupervisorID = context.ProgramSupervisors.FirstOrDefault(p => p.LastName == "Borris").ID
                    });
                    context.SaveChanges();
                }
                //Parent
                if (!context.Parents.Any())
                {
                    context.Parents.AddRange(
                        new Parent
                        {
                            FirstName = "Steve",
                            LastName = "Buscemi",
                            Email = "stevebuscemi@yahoo.com",
                            Phone = 2894923456
                        },

                        new Parent
                        {
                            FirstName = "Kenny",
                            LastName = "Rogers",
                            Email = "kennyrogers@gmail.com",
                            Phone = 9056578976
                        },

                        new Parent
                        {
                            FirstName = "Robert",
                            LastName = "De Niro",
                            Email = "taxidriver@hotmail.com",
                            Phone = 2895849054
                        },

                        new Parent
                        {
                            FirstName = "Alec",
                            LastName = "Baldwin",
                            Email = "alecbalwinny@yahoo.com",
                            Phone = 9056754534
                        },

                        new Parent
                        {
                            FirstName = "Tracy",
                            LastName = "Morgan",
                            Email = "tracymorgan@outlook.com",
                            Phone = 9054788989
                        },

                        new Parent
                        {
                            FirstName = "Michael",
                            LastName = "Scott",
                            Email = "theoffice@outlook.com",
                            Phone = 2893895465
                        },

                        new Parent
                        {
                            FirstName = "Rob",
                            LastName = "Stark",
                            Email = "rstark@gmail.com",
                            Phone = 2896758789
                        },

                        new Parent
                        {
                            FirstName = "Kevin",
                            LastName = "Hart",
                            Email = "hartK@hotmail.com",
                            Phone = 5048765675
                        });
                    context.SaveChanges();
                }

                //Parent Programs
                if (!context.Programs_Parents.Any())
                {
                    for (int i = 1; i <= 8; i++)
                    {
                        Random random = new Random();
                        int num = random.Next(1, 8);
                        for (int j = 1; j <= num; j++)
                        {
                            context.Add(
                                new Program_Parent
                                {
                                    ProgramID = i,
                                    ParentID = j
                                }
                            );
                        }
                    }
                    context.SaveChanges();
                }

                // Activity
                if (!context.Activities.Any())
                {
                    context.Activities.AddRange(
                        // adult swimming lane
                        new Activity
                        {
                            Title = "Women's Free Swim 1",
                            Description = "Session for the women's divsion of the program, be sure to come prepared as usual.",
                            Date = DateTime.Now.AddDays(4),
                            ProgramID = 1
                        },
                        new Activity
                        {
                            Title = "Women's Free Swim 2",
                            Description = "Session for the women's divsion of the program, be sure to come prepared as usual.",
                            Date = DateTime.Now.AddDays(8),
                            ProgramID = 1
                        },
                        new Activity
                        {
                            Title = "Women's Free Swim 3",
                            Description = "Session for the women's divsion of the program, be sure to come prepared as usual.",
                            Date = DateTime.Now.AddDays(12),
                            ProgramID = 1
                        },
                        new Activity
                        {
                            Title = "Women's Free Swim 4",
                            Description = "Session for the women's divsion of the program, be sure to come prepared as usual.",
                            Date = DateTime.Now.AddDays(16),
                            ProgramID = 1
                        },
                        new Activity
                        {
                            Title = "Women's Free Swim 5",
                            Description = "Session for the women's divsion of the program, be sure to come prepared as usual.",
                            Date = DateTime.Now.AddDays(20),
                            ProgramID = 1
                        },
                        new Activity
                        {
                            Title = "Men's Free Swim 1",
                            Description = "Session for the men's divsion of the program, be sure to come prepared as usual.",
                            Date = DateTime.Now.AddDays(2),
                            ProgramID = 1
                        },
                        new Activity
                        {
                            Title = "Men's Free Swim 2",
                            Description = "Session for the men's divsion of the program, be sure to come prepared as usual.",
                            Date = DateTime.Now.AddDays(6),
                            ProgramID = 1
                        },
                        new Activity
                        {
                            Title = "Men's Free Swim 3",
                            Description = "Session for the men's divsion of the program, be sure to come prepared as usual.",
                            Date = DateTime.Now.AddDays(10),
                            ProgramID = 1
                        },
                        new Activity
                        {
                            Title = "Men's Free Swim 4",
                            Description = "Session for the men's divsion of the program, be sure to come prepared as usual.",
                            Date = DateTime.Now.AddDays(14),
                            ProgramID = 1
                        },
                        new Activity
                        {
                            Title = "Men's Free Swim 5",
                            Description = "Session for the men's divsion of the program, be sure to come prepared as usual.",
                            Date = DateTime.Now.AddDays(18),
                            ProgramID = 1
                        },
                        // swimmer 1-6
                        new Activity
                        {
                            Title = "Junior Division Lesson 1",
                            Description = "Lesson for the junior divsion of the program, be sure to come prepared as usual.",
                            Date = DateTime.Now.AddDays(1),
                            ProgramID = 1
                        },
                        new Activity
                        {
                            Title = "Junior Division Lesson 2",
                            Description = "Lesson for the junior divsion of the program, be sure to come prepared as usual.",
                            Date = DateTime.Now.AddDays(3),
                            ProgramID = 1
                        },
                        new Activity
                        {
                            Title = "Junior Division Lesson 3",
                            Description = "Lesson for the junior divsion of the program, be sure to come prepared as usual.",
                            Date = DateTime.Now.AddDays(5),
                            ProgramID = 1
                        },
                        new Activity
                        {
                            Title = "Junior Division Lesson 4",
                            Description = "Lesson for the junior divsion of the program, be sure to come prepared as usual.",
                            Date = DateTime.Now.AddDays(7),
                            ProgramID = 1
                        },
                        // ball hockey
                        new Activity
                        {
                            Title = "Game 1 of Juniors",
                            Description = "Game for the junior ball hockey players, good luck everyone and be sure to come prepared!",
                            Date = DateTime.Now.AddDays(3),
                            ProgramID = 2
                        },
                        new Activity
                        {
                            Title = "Game 2 of Juniors",
                            Description = "Game for the junior ball hockey players, good luck everyone and be sure to come prepared!",
                            Date = DateTime.Now.AddDays(6),
                            ProgramID = 2
                        },
                        new Activity
                        {
                            Title = "Game 1 of Seniors",
                            Description = "Game for the senior ball hockey players, good luck everyone and be sure to come prepared!",
                            Date = DateTime.Now.AddDays(4),
                            ProgramID = 2
                        },
                        new Activity
                        {
                            Title = "Game 2 of Seniors",
                            Description = "Game for the senior ball hockey players, good luck everyone and be sure to come prepared!",
                            Date = DateTime.Now.AddDays(7),
                            ProgramID = 2
                        },
                        // basketball
                        new Activity
                        {
                            Title = "Game 1 of Juniors",
                            Description = "Game for the junior basketball players, good luck everyone and be sure to come prepared!",
                            Date = DateTime.Now.AddDays(2),
                            ProgramID = 3
                        },
                        new Activity
                        {
                            Title = "Game 2 of Juniors",
                            Description = "Game for the junior basketball players, good luck everyone and be sure to come prepared!",
                            Date = DateTime.Now.AddDays(9),
                            ProgramID = 3
                        },
                        new Activity
                        {
                            Title = "Game 3 of Juniors",
                            Description = "Game for the junior basketball players, good luck everyone and be sure to come prepared!",
                            Date = DateTime.Now.AddDays(16),
                            ProgramID = 3
                        },
                        new Activity
                        {
                            Title = "Game 1 of Seniors",
                            Description = "Game for the senior basketball players, good luck everyone and be sure to come prepared!",
                            Date = DateTime.Now.AddDays(4),
                            ProgramID = 3
                        },
                        new Activity
                        {
                            Title = "Game 1 of Seniors",
                            Description = "Game for the senior basketball players, good luck everyone and be sure to come prepared!",
                            Date = DateTime.Now.AddDays(11),
                            ProgramID = 3
                        },
                        new Activity
                        {
                            Title = "Game 1 of Seniors",
                            Description = "Game for the senior basketball players, good luck everyone and be sure to come prepared!",
                            Date = DateTime.Now.AddDays(18),
                            ProgramID = 3
                        },
                        // gymnastics 9-12 yrs
                        new Activity
                        {
                            Title = "Lesson 1 - Basics",
                            Description = "Covering basic beginnings, come with the proper change of clothes to get a bit chalky.",
                            Date = DateTime.Now.AddDays(2),
                            ProgramID = 4
                        },
                        new Activity
                        {
                            Title = "Lesson 2 - The Cartwheel",
                            Description = "Covering how to cartwheel, come with the proper change of clothes to get a bit chalky.",
                            Date = DateTime.Now.AddDays(9),
                            ProgramID = 4
                        },
                        new Activity
                        {
                            Title = "Lesson 1 - Standing Triple Twist Double Backflips",
                            Description = "Covering unrealistic gymnastical abilities for 9-12 year olds to learn, come with the proper change of clothes to get a bit chalky.",
                            Date = DateTime.Now.AddDays(16),
                            ProgramID = 4
                        },
                        // fort erie teen zone
                        new Activity
                        {
                            Title = "Spring Blast!!",
                            Description = "Get ready for an extremely fun night with the teen zone!",
                            Date = DateTime.Now.AddDays(5),
                            ProgramID = 5
                        },
                        // jr dance
                        new Activity
                        {
                            Title = "Modern Dance",
                            Description = "Modern style at a modern pace.",
                            Date = DateTime.Now.AddDays(3),
                            ProgramID = 6
                        },
                        new Activity
                        {
                            Title = "Line-style Dance",
                            Description = "Line dancing, country style.",
                            Date = DateTime.Now.AddDays(10),
                            ProgramID = 6
                        },
                        new Activity
                        {
                            Title = "Dance For Fun",
                            Description = "Let's blow off some steam everyone, had a stressful couple weeks.",
                            Date = DateTime.Now.AddDays(17),
                            ProgramID = 6
                        },
                        // bronze star
                        new Activity
                        {
                            Title = "Bronze Star Level Training 1",
                            Description = "Bronze Star Training",
                            Date = DateTime.Now.AddDays(3),
                            ProgramID = 7
                        });
                    context.SaveChanges();
                }
                if (!context.Announcements.Any())
                {
                    context.Announcements.AddRange(
                        // adult swimming lane
                        new Announcement
                        {
                            Title = "Women's Free Swim 1",
                            Body = "Posted the times, please refer to the activities schedule on the program page!",
                            ProgramID = 1,
                            CreatedOn = DateTime.Now.AddDays(-5)
                        },
                        new Announcement
                        {
                            Title = "Men's Free Swim 1",
                            Body = "Decided on a time for this week, get in touch if it doesn't work for you.",
                            ProgramID = 1,
                            CreatedOn = DateTime.Now.AddDays(-7)
                        },
                        // swimmer 1-6
                        new Announcement
                        {
                            Title = "Be prepared for our first lesson!",
                            Body = "Don't forget to bring all of the required equipment, and you might want to bring a change of clothes + towels for after the swim!",
                            ProgramID = 2,
                            CreatedOn = DateTime.Now.AddDays(-1)
                        },
                        new Announcement
                        {
                            Title = "Lost Wallet at Pool",
                            Body = "There was a wallet found at the pool last event. If you have a black leather wallet with a pink lace hanging off of it, talk to the pool owners please.",
                            ProgramID = 2,
                            CreatedOn = DateTime.Now.AddDays(-3)
                        },
                        // ball hockey
                        new Announcement
                        {
                            Title = "Cancellation",
                            Body = "Ball Hockey this Friday is cancelled due to sickness, my appologies everyone!",
                            ProgramID = 3,
                            CreatedOn = DateTime.Now.AddDays(-4)
                        },
                        new Announcement
                        {
                            Title = "Your child forget their stick?",
                            Body = "Someone forgot their ball hockey stick last game. It is a green CCM stick.",
                            ProgramID = 3,
                            CreatedOn = DateTime.Now.AddDays(-9)
                        },
                        // basketball
                        new Announcement
                        {
                            Title = "Game next wednesday!",
                            Body = "Don't forget that there is a game next wednesday, parents are more than welcome to come watch if you'd like!",
                            ProgramID = 4,
                            CreatedOn = DateTime.Now.AddDays(-1)
                        },
                        new Announcement
                        {
                            Title = "Fundraiser for tournament!",
                            Body = "Just a reminder that the fundraising event will be held this saturday at 1pm. Let's try to raise some money for this torunament!",
                            ProgramID = 4,
                            CreatedOn = DateTime.Now.AddDays(-2)
                        },
                        // Gymnastics 9-12
                        new Announcement
                        {
                            Title = "Gymnastics Practice moved",
                            Body = "Practice has been moved to next Tuesday this week!",
                            ProgramID = 5,
                            CreatedOn = DateTime.Now.AddDays(-3)
                        },
                        new Announcement
                        {
                            Title = "Competition",
                            Body = "Reminder that the competition is going to be held this saturday!",
                            ProgramID = 5,
                            CreatedOn = DateTime.Now.AddDays(-5)
                        },
                        // Fort Erie Teen Zone
                        new Announcement
                        {
                            Title = "Magic Day!",
                            Body = "This weeks Teen Zone will be magic themed! With a special appearance from Jimmy Chaotic!",
                            ProgramID = 6,
                            CreatedOn = DateTime.Now.AddDays(-4)
                        },
                        new Announcement
                        {
                            Title = "Cancelled",
                            Body = "Hello Everyone, this weeks Teen Zone will unfortunately have to be cancelled due to unforseen circumstances.",
                            ProgramID = 6,
                            CreatedOn = DateTime.Now.AddDays(-11)
                        },
                        // JR Dance
                        new Announcement
                        {
                            Title = "Best date for recital",
                            Body = "Just wondering what the best time of day is to have the recital so that the most parents can make it!",
                            ProgramID = 7,
                            CreatedOn = DateTime.Now.AddDays(-5)
                        },
                        new Announcement
                        {
                            Title = "Dance Day",
                            Body = "Parents are welcome to come to next weeks Dance Day, everyone will be dancing!",
                            ProgramID = 7,
                            CreatedOn = DateTime.Now.AddDays(-9)
                        },
                        // Bronze Star
                        new Announcement
                        {
                            Title = "Lost phone",
                            Body = "Someone forgot their phone last week. It will be at reception",
                            ProgramID = 8,
                            CreatedOn = DateTime.Now.AddDays(-2)
                        },
                        new Announcement
                        {
                            Title = "Potential change of date",
                            Body = "We will maybe be changing the weekly dates. I will keep you guys posted with any updates.",
                            ProgramID = 8,
                            CreatedOn = DateTime.Now.AddDays(-6)
                        },
                        // global testing
                        new Announcement
                        {
                            Title = "Global Announcement Test",
                            Body = "This will be going out to all parents and activities",
                            CreatedOn = DateTime.Now.AddDays(-2)
                        },
                        new Announcement
                        {
                            Title = "Open pool this Saturday!",
                            Body = "The pool at the Niagara Falls Location will be open to all this Saturday. You are all welcome to come!",
                            CreatedOn = DateTime.Now.AddDays(-2)
                        });
                    context.SaveChanges();
                }
            }
        }
    }
}