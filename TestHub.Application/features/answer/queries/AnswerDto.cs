using Application.common.models;

namespace Application.features.answer.queries;

public class AnswerDto
{
    public string Text { get; set; } = string.Empty;
    public FileDto? Image { get; set; }
    public bool IsCorrect { get; set; }
    public bool IsStrictText { get; set; }
}