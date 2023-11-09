// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
export class Resizer {
  public GetMaxHeight (frameID): number {
    const posX = getAbsoluteY(frameID)
    const avX = getBrowserHeight()

    let height = avX - posX - 50
    if (height < 200) { height = 200 }

    return height
  }
}

function getFrameSize (frameID) {
  const result = { height: 0, width: 0 }
  if (document.getElementById) {
    const fr = document.getElementById(frameID)
    if (fr.scrollWidth) {
      result.height = fr.scrollHeight
      result.width = fr.scrollWidth
    }
  }
  return result
}

// Findet die absolute y Position eines Elementes raus
function getAbsoluteY (elm) {
  let y = 0
  if (elm && typeof elm.offsetParent !== 'undefined') {
    while (elm && typeof elm.offsetTop === 'number') {
      y += elm.offsetTop
      elm = elm.offsetParent
    }
  }
  return y
}

// Findet die absolute y Position eines Elementes raus
function getBrowserHeight () {
  const rval = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight
  return rval
}
