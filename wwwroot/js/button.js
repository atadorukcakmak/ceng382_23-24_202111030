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

function HideShow2Function(){
  var x=document.getElementById("result")
  if(x.style.display==="none"){
      x.style.display="none";
      console.log("none");
  }else {
      x.style.display="block";
      console.log("block");
  }
  
}

function toggleBox() {
  document.getElementById("result").style.display = "block";
  
  const num1 = document.getElementById('num1').value;
  const num2 = document.getElementById('num2').value;
  const sum = parseInt(num1) + parseInt(num2);
  document.getElementById('result').innerText = `Toplam: ${sum}`;

}
