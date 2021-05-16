// VARIABLE & FUNCTION DECLARATIONS
//var AddAttribute = AddAttribute || {};
//var AddSkill = AddSkill || {};
//var AddPower = AddPower || {};

on('ready',function() {
	'use strict';
	log('-=> ImportMonster4e <=-');
});

//!c @{selected|token_id}$name$hp$AC$fort$ref$will$ability1$ability2
on("chat:message", function (msg) {
      // Exit if not an api command

  	if (msg.type != "api") return;
  	      
  	// Get the API Chat Command
  	msg.who = msg.who.replace(" (GM)", "");
  	msg.content = msg.content.replace("(GM) ", "");
  	var command = msg.content.split(" ", 1);

  	if (command != "!c") return;

  	if (!msg.selected) {
  	    sendChat("ERROR", "No object selected");
  	    return;
  	}

  	var n = msg.content.split(" ");

  	var Token = getObj("graphic", n[1].split("$",1));

  	if (Token.get("subtype") != "token") { 
  	    sendChat("ERROR", "Token not found");
  	    return;
  	}
  	
  	var n = msg.content.split("$");
  
  	//!c$@{selected|token_id}$name$hp$AC$fort$ref$will$ability1$ability2
  	/* C# code:
  	        sb.Append("!c @{selected|token_id}$$");
            sb.Append(monstername + "$");
            sb.Append(level + "$");
            sb.Append(perception + "$");
            sb.Append(HP + "$");
            sb.Append(initiative + "$");
            sb.Append(ac + "$");
            sb.Append(fort + "$");
            sb.Append(reflex + "$");
            sb.Append(will); */
            
    var MonsterName = n[2];
    var MonsterLevel = n[3];
    var Perception = n[4];
    var HitPoints = n[5];
    var Initiative = n[6];
    var DEF_AC = n[7];
    var DEF_FORT = n[8];
    var DEF_REF = n[9];
    var DEF_WILL = n[10];
    var ABILITY_START = 11;
     
    var debug = 0;
    if (debug==1) for (var i = ABILITY_START; i < n.length; i++) {
        var abname = GetMidString(n[i],"name=","}}");
        var str = ExtractPower(n[i]);
          
        sendChat("NAME = ", str);
		sendChat("POWER = ", abname);
		return;
    }
        
    // CHECK FOR DUPLICATE CHARACTERS
	var CheckSheet = findObjs({
		_type: "character",
		name: MonsterName
	});
    
	// DO NOT CREATE IF SHEET EXISTS
	if (CheckSheet.length > 0) {
		sendChat("ERROR", "This monster already exists.");
		return;
	}  
      
  	// CREATE CHARACTER SHEET & LINK TOKEN TO SHEET
  	var Character = createObj("character", {
  		avatar: Token.get("imgsrc"),
  		name: MonsterName,
  		gmnotes: Token.get("gmnotes"),
  		archived: false
  	});
  	
  	// Set HP
  	createObj("attribute", {
        name: 'hp',
        current: HitPoints,
        max: HitPoints,
        characterid: Character.id
    });
    
    createObj("attribute", {
        name: 'init-misc',
        current: Initiative - Math.floor(MonsterLevel/2),
        characterid: Character.id
    });
    
    createObj("attribute", {
        name: 'level',
        current: MonsterLevel,
        characterid: Character.id
    });
    
    createObj("attribute", {
        name: 'perception-misc',
        current: Perception - Math.floor(MonsterLevel/2),
        characterid: Character.id
    });
    
    createObj("attribute", {
        name: 'ac-misc',
        current: DEF_AC - Math.floor(MonsterLevel/2)-10,
        characterid: Character.id
    });
    
    createObj("attribute", {
        name: 'fort-misc',
        current: DEF_FORT - Math.floor(MonsterLevel/2)-10,
        characterid: Character.id
    });
    
    createObj("attribute", {
        name: 'ref-misc',
        current: DEF_REF - Math.floor(MonsterLevel/2)-10,
        characterid: Character.id
    });
    
    createObj("attribute", {
        name: 'will-misc',
        current: DEF_WILL - Math.floor(MonsterLevel/2)-10,
        characterid: Character.id
    });

  	// SET TOKEN VALUES
  	Token.set("represents", Character.id);
  	Token.set("name", MonsterName);
  	Token.set("showplayers_name", true);
  	Token.set("bar3_max", HitPoints);
  	Token.set("bar3_value", HitPoints);
  	Token.set("showplayers_bar3", true);
  	Token.set("showplayers_aura1", true);
    Token.set("aura1_radius", " ");
  	Token.set("aura1_color", "#660000");
  	Token.set("aura1_square", true);
  	Token.set("aura2_square", true);

    // add abilities
    for (var i = ABILITY_START; i < n.length; i++) {
      var abname = GetMidString(n[i],"name=","}}");
      var str =  n[i];
      var str = str.replace(/(\r\n|\n|\r)/gm,"");
      var str = str.replace(/XX/igm, "[[");
      var str = str.replace(/ZZ/igm, "]]");
      var str = str.replace(/T4T/igm, "&{template:dnd4epower}");
      var str = str.replace(/T4E/igm, "&{template:dnd4epower}");
      
      createObj("ability", {
    		name: abname,
    		description: "",
    		action: str,
    		istokenaction: true,
    		characterid: Character.id
    	
      });
    }
  	
	return;
});
      
function GetMidString(str, value1, value2) {
  var str1 = str.substring(str.indexOf(value1)+value1.length,str.length);
  var result = str1.substring(0,str1.indexOf(value2));
  return result;
} 

function ExtractPower(input) {
    var str = input;
    var str = str.replace(/(\r\n|\n|\r)/gm,"");
    var str = str.replace(/XX/igm, "[[");
    var str = str.replace(/ZZ/igm, "]]");
    var str = str.replace(/T4E/igm, "&{template:dnd4epower}");
    var str = str.replace(/T4T/igm, "&{template:dnd4epower}");
    return str;
}
      
      
function AddAttribute(attr, value, charid) {    
if (attr === "Hit Points") {
	createObj("attribute", {
		name: attr,
		current: value,
		max: value,
		characterid: charid
	});
} else {
	createObj("attribute", {
		name: attr,
		current: value,
		characterid: charid
	});
}
return;
}

function AddSkill(monstername, skill, value, charid, showtrained) {
	createObj("ability", {
		name: skill + showtrained,
		description: "",
		action: '/as "' + monstername + '" ' + skill + ' [[1d20 + ' + value + ']]',
		istokenaction: false,
		characterid: charid
	});
}

function AddPower(mpname, powerstring, charid) {
	createObj("ability", {
		name: mpname,
		description: "",
		action: powerstring,
		istokenaction: true,
		characterid: charid
	});
}