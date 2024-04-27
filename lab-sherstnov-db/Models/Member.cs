using System;
using System.Collections.Generic;

namespace lab_sherstnov_db.Models;

public partial class Member
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateOnly DateJoined { get; set; }

    public virtual ICollection<ClassRegistration> ClassRegistrations { get; set; } = new List<ClassRegistration>();

    public virtual ICollection<MemberTrainer> MemberTrainers { get; set; } = new List<MemberTrainer>();
}
