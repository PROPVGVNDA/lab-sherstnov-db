using System;
using System.Collections.Generic;

namespace lab_sherstnov_db.Models;

public partial class Class
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int TrainerId { get; set; }

    public DateTime ClassTime { get; set; }

    public int Duration { get; set; }

    public int? MaximumParticipants { get; set; }

    public virtual ICollection<ClassRegistration>? ClassRegistrations { get; set; } = new List<ClassRegistration>();

    public virtual Trainer? Trainer { get; set; } = null!;
}
