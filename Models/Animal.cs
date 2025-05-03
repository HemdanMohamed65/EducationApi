using System;
using System.Collections.Generic;

namespace EduApplication.Models;

public partial class Animal
{
    public int animal_id { get; set; }

    public int category_id { get; set; }

    public string animal_name { get; set; }

    public string audio_url { get; set; }

    public string photo_url { get; set; }

    public virtual Category category { get; set; }
}