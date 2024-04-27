using System;
using System.Collections.Generic;

namespace lab_sherstnov_db.Models;

public partial class Trainer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Specialization { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<MemberTrainer> MemberTrainers { get; set; } = new List<MemberTrainer>();
}
