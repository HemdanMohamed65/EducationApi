using System;
using System.Collections.Generic;

namespace EduApplication.Models;

public partial class Child_Content
{
    public int id { get; set; }

    public int child_id { get; set; }

    public int category_id { get; set; }

    public int content_id { get; set; }

    public virtual Category category { get; set; }

    public virtual Child child { get; set; }
}