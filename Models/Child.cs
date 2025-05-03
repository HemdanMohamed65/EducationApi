using System;
using System.Collections.Generic;

namespace EduApplication.Models;

public partial class Child
{
    public int child_id { get; set; }

    public int? parent_id { get; set; }

    public string name { get; set; }

    public int age { get; set; }

    public virtual ICollection<Child_Content> Child_Contents { get; set; } = new List<Child_Content>();

    public virtual Parents parent { get; set; }
}