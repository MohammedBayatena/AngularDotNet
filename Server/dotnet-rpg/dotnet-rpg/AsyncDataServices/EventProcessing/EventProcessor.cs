using dotnet_rpg.Contracts;
using dotnet_rpg.Contracts.Models.SkillModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;

namespace dotnet_rpg.AsyncDataServices.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public EventProcessor(IServiceScopeFactory scopeFactory, AutoMapper.IMapper mapper)
        {
            _scopeFactory = scopeFactory;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.SkillPublished:
                    addPlatform(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);

            switch (eventType.Event)
            {
                case "Skill_Published":
                    Console.WriteLine("--> Skill Published Event Detected");
                    return EventType.SkillPublished;

                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void addPlatform(string platformPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                //var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                var platformPublishedDto = JsonSerializer.Deserialize<SkillPublishModel>(platformPublishedMessage);
                try
                {
                    Console.WriteLine($"--> Recived Skill Object With Id: {platformPublishedDto.Id} ,Name: {platformPublishedDto.Name} ");
                    //var plat = _mapper.Map<Platform>(platformPublishedDto);
                    //if (!repo.ExternalPlatformExists(plat.ExternalID))
                    //{
                        //repo.CreatePlatform(plat);
                        //repo.SaveChanges();
                        //Console.WriteLine("--> Platform added!");
                    //}
                    //else
                    //{
                    //    Console.WriteLine("--> Platform already exisits...");
                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Platform to DB {ex.Message}");
                }
            }
        }
    }

    internal enum EventType
    {
        SkillPublished,
        Undetermined
    }
}