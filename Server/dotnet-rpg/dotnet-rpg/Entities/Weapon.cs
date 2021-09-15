using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TrackableEntities.Common.Core;

namespace dotnet_rpg.Entities
{
    public class Weapon : ITrackable, IMergeable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }

        public Character Character { get; set; }
        public int CharacterId { get; set; }

        [NotMapped]
        public TrackingState TrackingState { get; set; }

        [NotMapped]
        public ICollection<string> ModifiedProperties { get; set; }

        [NotMapped]
        public Guid EntityIdentifier { get; set; }
    }
}