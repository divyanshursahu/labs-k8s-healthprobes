using System.ComponentModel.DataAnnotations;

namespace BrezyWeather.Models;

public class City
{
    public int  ID { get; set; }

    public string? Name { get; set; }

    public string? ZipCode { get; set; }
}

