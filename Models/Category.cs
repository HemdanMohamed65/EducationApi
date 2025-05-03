using System;
using System.Collections.Generic;

namespace EduApplication.Models;

public partial class Category
{
    public int category_id { get; set; }

    public string category_name { get; set; }

    public virtual ICollection<Alphabet> Alphabets { get; set; } = new List<Alphabet>();

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();

    public virtual ICollection<Child_Content> Child_Contents { get; set; } = new List<Child_Content>();

    public virtual ICollection<Number> Numbers { get; set; } = new List<Number>();

    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();
}