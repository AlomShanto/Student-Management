﻿using System;

namespace Server.Contracts.Models;

public class UserForm
{
    public string Username { get; set; }
    public string Role { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}