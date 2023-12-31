﻿namespace TestHub.Core.Models;

public class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAd { get; set; }

    public DateTime DeleteAt { get; set; }

    public string Comment { get; set; } = null!;

    public virtual ICollection<TestMetadata> TestMetadata { get; set; } = new List<TestMetadata>();

    public virtual ICollection<TestSession> TestSessions { get; set; } = new List<TestSession>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
