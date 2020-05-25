# OptionMenuCreator

## Idea
When this project is done, it will allow you to create an OptionMenu from just a class with properties you want to edit.
It will work with Attributes to determine what input type is required for which property.
Like this:
```c#
class SampleOptionMenu : OptionMenu
{
  [Slider(0, 9.99)] //Slider range 0 to 9.99
  public double SampleSlider;
  [NumberBox(-1000, 1000)] //Number range -1000 to 1000
  public int SampleNumberBox;
  [Textbox(false, 16)] //Text without multiline and limited to 16 Chars
  public string SampleTextbox;
  
  //If you want to make public proporties not Appear in the OptionMenu use the NotEditable Attribute
  [NotEditable]
  public string HiddenString = "I'm totaly hidden, don't mind me!";
}
```

## Roadmap
List of all the things I want to do with this project and their current state 
### Currently Working
* Creating the window asynchronously
* Attributes 
### Planed
* Adding Labels etc. to the window 
### Considered
* Adding support for complex data structures like structs and classes to appear in the option menu
