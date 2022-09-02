using System.Threading.Tasks;
using static QuizAPI;

public static class ConsumeTest
{
    public static async Task<Root> GetNewQuestions()
    {
        // 15 = Videogames - 11 = Films - 12 = Music - 14 = Television - 10 = Books
        var url = "https://opentdb.com/api.php?amount=3&category=11&difficulty=easy&type=multiple";
        var httpClient = new HttpClient(new JsonSerializationOption());

        var result = await httpClient.Get<Root>(url);
        return result;
    }
}
