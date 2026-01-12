public class SkillInven
{
    private List<Skill> _skills = new List<Skill>();
    public bool IsActive { get; set; }
    public MenuList _skillMenu = new MenuList();
    private PlayerCharacter _owner;
    
    public SkillInven(PlayerCharacter owner)
    {
        _owner = owner;
    }

    public void Add(Skill skill)
    {
        if (_skills.Count >= 5) return;
        
        _skills.Add(skill);
        _skillMenu.Add(skill.Name, skill.Use);
        skill._skillinven = this;
        skill.Owner = _owner;
    }

    public void Remove(Skill skill)
    {
        _skills.Remove(skill);
        _skillMenu.Remove();
    }

    public void Render()
    {
        if (!IsActive) return;
        
        _skillMenu.Render(10, 15);
    }

    public void Select()
    {
        if(!IsActive) return;
        _skillMenu.Select();
    }

    public void SelectUp()
    {
        if(!IsActive) return;
        _skillMenu.SelectUp();
    }

    public void SelectDown()
    {
        if(!IsActive) return;
        _skillMenu.SelectDown();
    }
}