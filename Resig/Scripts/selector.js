
// Creates a selector centric api for manipluating the HTML elements
// similar to the functionality provided by jQuery.
//
// selector - A CSS selector.
function $(selector) {
  return resigQuery.select(selector);
};