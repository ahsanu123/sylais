namespace Sylais.Models;

public class PiperRequest
{
    public string text { get; set; } = "";          // text (required) - text to synthesize
    public string? voice { get; set; }              // voice (optional) - name of voice to use; defaults to -m <VOICE>
    public string? speaker { get; set; }            // speaker (optional) - name of speaker for multi-speaker voices
    public string? speaker_id { get; set; }         // speaker_id (optional) - id of speaker for multi-speaker voices; overrides speaker 
    public string? length_scale { get; set; }       // length_scale (optional) - speaking speed; defaults to 1
    public string? noise_scale { get; set; }        // noise_scale (optional) - speaking variability
    public string? noise_w_scale { get; set; }      // noise_w_scale (optional) - phoneme width variability

}
