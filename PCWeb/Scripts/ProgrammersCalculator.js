
  //Convert Decimal to Binary
  function toBinStr(decval) {
      nbb=32; //32bit integers

      tmpChar="";
      //Check if negative value
      neg = false;
      if (decval<0) {
          decval = -decval;
          neg = true;
      }
      //Binary Code of the positive value
      for(i=nbb-1;i>=0;i--) {
          if ((decval / Math.pow(2,i)) >= 1) {
              tmpChar = tmpChar + "1";
              decval = decval % Math.pow(2,i);
          } else
              tmpChar = tmpChar + "0";
      }
      if (neg) {
          //Complement Code
          tmp2="";
          for (i=0;i<nbb;i++) {
              if (tmpChar.charAt(i)=="0") {
                  tmp2 += "1" ;
              } else {
                  tmp2 += "0";
              }
          }
          tmpChar = tmp2;
          //Two's Complement Code
          tmp2="";
          ovf = 1;
          i   =nbb-1;
          while ((ovf==1)&&(i>=0)) {
              b = 1 ^ tmpChar.charAt(i);
              ovf = tmpChar.charAt(i);
              tmp2 = b.toString() + tmp2;
              i--;
          }
          for (;i>=0;i--) {
              tmp2 = tmpChar.charAt(i) + tmp2;
          }
          tmpChar = tmp2;
      }

      return tmpChar;
  }

function TestIt(fm, operation) {
    o1 = fm.op1.value;
    o2 = fm.op2.value;

    //If EmptyFields||NonDigitFields { ErrorMsg("err") }
    reNonDigital = /[^\d\+-]/;
    if ((fm.op1.value=="")||(reNonDigital.exec(fm.op1.value)!=null)) {
        alert("Enter decimal value!");
        fm.op1.focus();
        return false;
    }
    if ((fm.op2.value=="")||(reNonDigital.exec(fm.op2.value)!=null)) {
        alert("Enter decimal value!");
        fm.op2.focus();
        return false;
    }
    //Calculate Result
    switch (operation) {
        case 6:
            fm.rs6.value = ~ fm.op1.value;
            fm.descr.value = " ~ " + toBinStr(o1) + " = " + toBinStr(fm.rs6.value);
            break;
        case 5:
            fm.rs5.value = fm.op1.value ^ fm.op2.value;
            fm.descr.value = toBinStr(o1) + " ^ " + toBinStr(o2) + " = " + toBinStr(fm.rs5.value);
            break;
        case 4:
            fm.rs4.value = fm.op1.value | fm.op2.value;
            fm.descr.value = toBinStr(o1) + " | " + toBinStr(o2) + " = " + toBinStr(fm.rs4.value);
            break;
        case 3:
            fm.rs3.value = fm.op1.value & fm.op2.value;
            fm.descr.value = toBinStr(o1) + " & " + toBinStr(o2) + " = " + toBinStr(fm.rs3.value);
            break;
        case 2:
            fm.rs2.value = fm.op1.value << fm.op2.value;
            fm.descr.value = toBinStr(o1) + " << " + o2 + " = " + toBinStr(fm.rs2.value);
            break;
        case 1:
            fm.rs1.value = fm.op1.value >>> fm.op2.value;
            fm.descr.value = toBinStr(o1) + " >>> " + o2 + " = " + toBinStr(fm.rs1.value);
            break;
        case 0:
        default:
            fm.rs0.value = fm.op1.value >> fm.op2.value;
            fm.descr.value = toBinStr(o1) + " >> " + o2 + " = " + toBinStr(fm.rs0.value);
    }

    //Display Binary Operation

    return true;
}


function doCrypt(isDecrypt) {
    var shiftText = document.getElementById("Output").value;
    if (!/^-?\d+$/.test(shiftText)) {
        alert("Shift is not an integer");
        return;
    }
    var key = parseInt(shiftText, 10);
    if (key < 0 || key >= 26) {
        alert("Shift is out of range");
        return;
    }
    if (isDecrypt)
        key = (26 - key) % 26;
    var textElem = document.getElementById("Input");
    textElem.value = crypt(textElem.value, key);
}


/*
 * Returns the result of having each letter of the given text shifted forward by the given key, with wraparound. Case is preserved, and non-letters are unchanged.
 * Examples:
 *   crypt("abz", 1) = "bca"
 *   crypt("THe 123 !@#$", 13) = "GUr 123 !@#$"
 */
function crypt(input, key) {
    var output = "";
    for (var i = 0; i < input.length; i++) {
        var c = input.charCodeAt(i);
        if (c >= 65 && c <= 90) output += String.fromCharCode((c - 65 + key) % 26 + 65);  // Uppercase
        else if (c >= 97 && c <= 122) output += String.fromCharCode((c - 97 + key) % 26 + 97);  // Lowercase
        else output += input.charAt(i);  // Copy
    }
    return output;
}