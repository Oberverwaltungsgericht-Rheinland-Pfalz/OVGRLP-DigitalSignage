export class Resizer {

    public GetMaxHeight(frameID): number {
      var posX = getAbsoluteY(frameID);
      var avX = getBrowserHeight();
  
      var height = avX - posX - 50;
      if (height < 200) { height = 200; }
  
      return height;
    }
  
  }
  
  function getFrameSize(frameID) {
    var result = { height: 0, width: 0 };
    if (document.getElementById) {
      var fr = document.getElementById(frameID);
      if (fr.scrollWidth) {
        result.height = fr.scrollHeight;
        result.width = fr.scrollWidth;
      }
    }
    return result;
  }
  
  // Findet die absolute y Position eines Elementes raus
  function getAbsoluteY(elm) {
    var y = 0;
    if (elm && typeof elm.offsetParent != "undefined") {
      while (elm && typeof elm.offsetTop == "number") {
        y += elm.offsetTop;
        elm = elm.offsetParent;
      }
    }
    return y;
  }
  
  // Findet die absolute y Position eines Elementes raus
  function getBrowserHeight() {
    var rval = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;
    return rval;
  }
  