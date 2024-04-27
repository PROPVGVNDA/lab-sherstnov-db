using System;
using System.Collections.Generic;

namespace lab_sherstnov_db.Models;

public partial class MemberTrainer
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int TrainerId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual Member? Member { get; set; } = null!;

    public virtual Trainer? Trainer { get; set; } = null!;
}
