
// Gets an element based on the CSS selector.
//
// selector - A CSS selector.
function $get(selector) {
  var elements = [],
      isId = false,
      element = null;
  if (selector) {
    isId = selector.indexOf("#") === 0;
    if (isId) {
      selector = selector.slice(1);
      element = _document.GetElementById(selector);
      elements.push(element);
    } else {
      elements = _document.QuerySelectorAll(selector);
      elements = _domHelper.ToArray(elements);
    }
  }
  return elements;
}

// Creates a selector centric api for manipluating the HTML elements
// similar to the functionality provided by jQuery.
//
// selector - A CSS selector.
function $(selector) {
  var resig = {
    selector: selector,
    length: 0,
    elements: []
  };

  // Gets the number of elements in the resig object.
  resig.size = function () {
    return resig.length;
  };

  // Get the current value of the first element in the set of matched 
  // elements or set the value of every matched element.
  //
  // value - The value to set.
  resig.val = function (value) {
    var returned = resig;
    if (value) {
      resig.elements.forEach(function (element) {
        element.Value = value;
        element.SetAttribute("value", value);
      });
    } else if (resig.length) {
      returned = resig.elements[0].Value;
    } else {
      returned = '';
    }
    return returned;
  };

  // Gets or sets text for all matched elements.
  //
  // text - the text to set.
  resig.text = function (text) {
    var value = resig;
    if (text) {
      resig.elements.forEach(function (element) {
        element.TextContent = text;
      });
    } else {
      value = '';
      resig.elements.forEach(function (element) {
        value += element.TextContent + ' '
      })
      value = value.trim();
    }
    return value;
  };

  // Gets the HTML contents of the first element in the set of matched 
  // elements or set the HTML contents of every matched element.
  //
  // html - The HTML to set.
  resig.html = function (html) {
    var value = resig;
    if (html) {
      resig.elements.forEach(function (element) {
        element.InnerHtml = html;
      });
    } else if (resig.length) {
      value = resig.elements[0].InnerHtml;
    } else {
      value = '';
    }
    return value;
  };

  resig.elements = $get(selector);
  resig.length = resig.elements.length;
  return resig;
};