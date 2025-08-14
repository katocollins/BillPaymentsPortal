// JavaScript Document
//<script type="text/javascript">
// Gallery script.
// With image cross fade effect for those browsers that support it.
// Script copyright (C) 2004-08 www.cryer.co.uk.
// Script is free to use provided this copyright header is included.
function LoadGallery(pictureName,imageFile,titleCaption,captionText)
{
  document.getElementById(pictureName).src = imageFile;
  document.getElementById(pictureName).innerHTML=captionText;
}
//</script>