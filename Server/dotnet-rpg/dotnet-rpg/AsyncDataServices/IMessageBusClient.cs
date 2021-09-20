using dotnet_rpg.Contracts.Models.SkillModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewSkill(SkillPublishModel skillPublishedModel);
    }
}
