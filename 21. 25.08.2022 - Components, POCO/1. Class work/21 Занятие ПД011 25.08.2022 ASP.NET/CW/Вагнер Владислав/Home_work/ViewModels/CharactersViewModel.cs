using Home_work.Models.Characters;

namespace Home_work.ViewModels;

public class CharactersViewModel
{
    //Коллекция персонажей
    public List<Character> Characters { get; set; }

    //Общее кол-во фотографий в альбоме
    public int Count { get; set; }

    public CharactersViewModel(List<Character> characters,int count)
    {
        Characters = characters;

        Count = count;
    }
}
