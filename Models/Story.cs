using System;
using System.Collections.Generic;

namespace EduApplication.Models;

public partial class Story
{
    public int story_id { get; set; }

    public int category_id { get; set; }

    public string title { get; set; }

    public string video_url { get; set; }

    public string photo_url { get; set; }

    public virtual Category category { get; set; }
}