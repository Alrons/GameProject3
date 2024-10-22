using System;

public class ChangeWaveRequest
{
    public int Id { get; set; }

    public int UserID { get; set; }

    public int WavesNumber { get; set; }

    public int DurationInSeconds { get; set; }

    public int WavesPower { get; set; }

    public DateTime StartWave { get; set; }

    public double Passing { get; set; }

    public double WaveEnd { get; set; }

    public int WaveHealth { get; set; }

    public int Status { get; set; }
}
