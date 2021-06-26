on("ready", function() 
{
    on("add:token", onAddToken);
    on("change:token:bar3_value", onChangeToken);
});

function onChangeToken(obj, prev) 
{
    //Do something with "obj" here. "prev" is a list of previous values.
    // Note that "obj" and "prev" are different TYPES of objects. 
    // to work with obj you need to use obj.get("name");
    // to work with prev you can use prev["name"];
    var char = getObj("character", obj.get("represents"));
    if (isPC(char))
        return;
    
    var newHP = obj.get("bar3_value");
    var maxHP = obj.get("bar3_max");
    var prevHP = prev["bar3_value"];
    
    if (newHP == null || prevHP == null || maxHP == null)
        return;
    
    setHealthTint(obj);
}

function setHealthTint(token)
{
    var newHP = token.get("bar3_value");
    var maxHP = token.get("bar3_max");
    
    token.set("aura1_square", true);
    token.set("showplayers_bar3", false);
    token.set("aura1_radius", "0");
    token.set("aura2_color", "transparent");
    
    if (newHP >= maxHP)
    {
        token.set("aura1_color", "#00FF00");
        log(1);
    }
    else if (newHP >= maxHP * 0.75)
    {
        token.set("aura1_color", "#FFFF00");
        log(2);
    }   
    else if (newHP >= maxHP * 0.5)
    {
        token.set("aura1_color", "#FF9900");
        log(3);
    }
    else if (newHP >= maxHP * 0.25)
    {
        token.set("aura1_color", "#FF0000");
        log(4);
    }
    else if (newHP > 0)
    {
        token.set("aura1_color", "#660000");
        log(5);
    }
    else if (newHP <= 0)
    {
        token.set("aura1_color", "transparent");
        token.set("status_dead", true);
        log(6);
        return;
    }
    
    log(7);
    token.set("status_dead", false);
}

function onAddToken(obj) 
{
    var char = getObj("character", obj.get("represents"));
    if (char)
    {
        if (!isPC(char))
            return;
        
        var hp = findObjs({_type: "attribute", characterid: char.id, name: "hp"})[0];

        if (hp)
        {
            var hpMax = hp.get("max");
        }
        
        obj.set("bar3_max", hpMax);
        obj.set("bar3_value", hpMax);
        obj.set("aura1_radius", 0);
        obj.set("aura1_color", "#00FF00");
        obj.set("aura1_square", true);
        obj.set("showplayers_bar3", false);
        obj.set("showplayers_aura1", true);
    }
}

function isPC(character)
{
    if (!character)
        return false;
    
    var charClass = findObjs({_type: "attribute", characterid: character.id, name:"class"})[0];
    return (charClass && charClass.get("current"));
}
