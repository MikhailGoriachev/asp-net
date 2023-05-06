using Home_work.Models.Characters;

namespace Home_work.ViewModels;

public class CharactersViewModel
{
    //Коллекция персонажей
    public List<Character> Characters { get; set; }

    //Общее кол-во персонажей
    public int Count { get; set; }

    public (double? avg, double? delta) Account { get; set; }

    public CharactersViewModel(List<Character> characters,int count, (double? avg, double? delta) accountments)
    {
        Characters = characters;

        Count = count;
        Account = accountments;
    }
}
