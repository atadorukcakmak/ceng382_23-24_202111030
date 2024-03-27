function HideShowFunction(){
    var x=document.getElementById("myDIV")
    if(x.style.display==="none"){
        x.style.display="block";
        console.log("block");
    }else {
        x.style.display="none";
        console.log("none");
    }
}

function ShowHideFunction(){
    var y=document.getElementById("myDIVshow")
    if(y.style.display==="none"){
        y.style.display="block";
        console.log("block");
    }else {
        y.style.display="none";
        console.log("none");
    }
}

function calculateSum() {
    var num1 = parseInt(document.getElementById("num1").value);
    var num2 = parseInt(document.getElementById("num2").value);
    var sum = num1 + num2;
    return sum;
    
}
