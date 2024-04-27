using System;
using System.Collections.Generic;

namespace lab_sherstnov_db.Models;

public partial class ClassRegistration
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int ClassId { get; set; }

    public DateOnly RegistrationDate { get; set; }

    public virtual Class? Class { get; set; } = null!;

    public virtual Member? Member { get; set; } = null!;
}
