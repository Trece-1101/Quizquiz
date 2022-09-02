using System.Threading.Tasks;
using static QuizAPI;

public static class ConsumeAPI
{
    public static async Task<Root> GetNewQuestions(int amount = 5, int category = 15, string difficulty = "easy")
    {
        // 15 = Videogames - 11 = Films - 12 = Music - 14 = Television - 10 = Books
        //var url = "https://opentdb.com/api.php?amount=3&category=11&difficulty=easy&type=multiple";

        var url = "https://opentdb.com/api.php?amount="+ amount +"&category=" + category + "&difficulty="+ difficulty +"&type=multiple";

        var httpClient = new HttpClient(new JsonSerializationOption());

        var result = await httpClient.Get<Root>(url);
        return result;
    }
}