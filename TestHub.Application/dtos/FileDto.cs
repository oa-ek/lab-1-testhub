﻿namespace Application.dtos;

public class FileDto
{
    public string FileName { get; set; }  
    public string ContentType { get; set; }  
    public byte[] Data { get; set; } 
}