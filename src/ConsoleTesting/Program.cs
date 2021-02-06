using Events.IO.Domain.Core.Commands;
using Events.IO.Domain.Core.Notifications;
using Events.IO.Domain.Events;
using Events.IO.Domain.Events.Commands;
using Events.IO.Domain.Events.Repository;
using Events.IO.Domain.Handlers;
using Events.IO.Domain.Interfaces;
using MediatR;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            // ValidTestSimple();
            RuntoTestDomain();
        }

        static void ValidTestSimple()
        {
            var occasion = new Occasion(
                "Venda de carros",
                DateTime.Now,
                DateTime.Now,
                true,
                0,
                true,
                "Chevrolett"
                );

            var occasion2 = new Occasion(
                "Venda de carros",
                DateTime.Now,
                DateTime.Now,
                true,
                0,
                true,
                "Chevrolett"
                );

            var occasion3 = occasion;

            // test validation
            var occasion4 = new Occasion(
                "",
                DateTime.Now.AddDays(-1),
                DateTime.Now.AddDays(-2),
                false,
                0,
                true,
                ""
                );

            Console.WriteLine(occasion.ToString());
            Console.WriteLine(occasion2.ToString());
            Console.WriteLine(occasion3.ToString());
            Console.WriteLine(occasion.Equals(occasion2));
            Console.WriteLine(occasion.Equals(occasion3));

            if (!occasion4.IsValid())
            {
                foreach (var item in occasion4.ValidationResult.Errors)
                {
                    Console.WriteLine($"Error: {item.ErrorMessage}");
                }
            }
        }

        static void RuntoTestDomain()
        {
            DependencyInjectionContainer.Inicialize();

            var mediator = DependencyInjectionContainer.container.GetInstance<IMediatorHanlder>();

            var command = new RegisterEventCommand("Venda de carros", DateTime.Now, DateTime.Now, true, 0, true, "Chevrolett");
            Start();
            mediator.SendCommand(command);
            End();



        }

        static void Start()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Startining test");
        }

        static void End()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Ending test");
            Console.WriteLine(string.Empty);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*********************************");
            Console.WriteLine(string.Empty);
        }
    }

    class DependencyInjectionContainer
    {
        public static Container container;

        public static void Inicialize()
        {
            //var assembly = AppDomain.CurrentDomain.Load("HR.Domain");
            //services.AddMediatR(assembly);

            container = new Container();

            // Register your types, for instance:
            container.Register<IMediatorHanlder, MediatorHanlder>();
            container.Register<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //container.Register<IRequestHandler<RegisterEventCommand, bool>, EventCommandHandler>();
            //container.Register<IRequestHandler<UpdateEventCommand, bool>, EventCommandHandler>();
            //container.Register<IRequestHandler<DeleteEventCommand, bool>, EventCommandHandler>();

            //container.Register<INotificationHandler<RegisteredEventEvent>, EventSystemHandlers>();
            //container.Register<INotificationHandler<UpdatedEventEvent>, EventSystemHandlers>();
            //container.Register<INotificationHandler<DeletedEventEvent>, EventSystemHandlers>();

            container.Register<IEventRepository, FakeRepository>(Lifestyle.Singleton);
            container.Register<IUnitOfWork, FakeUnitOfWork>(Lifestyle.Singleton);


            container.Register<IMediator, Mediator>();

            //container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            //{
            //    typeof(RequestExceptionProcessorBehavior<,>),
            //    typeof(RequestExceptionActionProcessorBehavior<,>),
            //    typeof(RequestPreProcessorBehavior<,>),
            //    typeof(RequestPostProcessorBehavior<,>)
            //});


            var assemblies = GetAssemblies().ToArray();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(INotificationHandler<>), assemblies);

            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);

            // Optionally verify the container.
            container.Verify();
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IMediator).GetTypeInfo().Assembly;
            yield return AppDomain.CurrentDomain.Load("Events.IO.Domain");
            //yield return AppDomain.CurrentDomain.Load("Events.IO.Domain.Core");
            //yield return typeof(Ping).GetTypeInfo().Assembly;
        }
    }



    class FakeRepository : IEventRepository
    {
        public void Add(Occasion obj)
        {
            //
        }

        public void Dispose()
        {
            //
        }

        public IEnumerable<Occasion> Find(Expression<Func<Occasion, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Occasion> GetAll()
        {
            throw new NotImplementedException();
        }

        public Occasion GetById(Guid id)
        {
            return new Occasion(
                "Venda de carros",
                DateTime.Now,
                DateTime.Now,
                true,
                0,
                true,
                "Chevrolett"
                );
        }

        public void Remove(Guid id)
        {
            //
        }

        public int SaveChanges()
        {
            return 1;
        }

        public void Update(Occasion obj)
        {
            //
        }
    }

    class FakeUnitOfWork : IUnitOfWork
    {
        public CommandResponse Comit()
        {
            return new CommandResponse(true);
        }

        public void Dispose()
        {
            //
        }
    }
}
