using System;
using System.Collections.Generic;

namespace EduApplication.Models;

public partial class Number
{
    public int number_id { get; set; }

    public int category_id { get; set; }

    public int number_value { get; set; }

    public string audio_url { get; set; }

    public string photo_url { get; set; }

    public virtual Category category { get; set; }
}