// What's available
// 
// document - An AngleSharp IHtmlDocument
// __env_selector - A Resig.Selector instance used to produce Resig.Match 
//                 objects.
// __env_log - An Action<string> function used for logging from Jint.  This
//             is aliased to console.log.
// $ - A pure JavaScript function which wraps __env_selector.Query.  This is
//     the jQuery like interface for model binding
// _ - The full build of lodash.  Mostly because I could.  This should 
//     definitely be optional, but it's very useful.

function lorem(model) {
  return 'lorem ipsum ' + model.Foo + ' ' + model.Bar + ' baz';
}

function bind(model, bag) {
  var loremIpsum = lorem(model),
      fooval = '';
  console.log('fooval: ' + fooval);
  $('.foo').val(model.Foo + ' and lorems');
  $('#bar').val(model.Bar);
  fooval =  $('.foo').val();
  console.log('fooval: ' + fooval);
  $('.bar').val(fooval);
  $('.baz').text(model.Baz + ' Mike');
  $('#baz').html('<em>' + loremIpsum + '</em>');
  $('.bacon').text(bag.Bacon + ' lazy dogs. ' + _.min([4, 2, 3, 6, 7]) + ', ' + _.now() + 2);

  $('#lodash-now').text(_.now());
  $('#moment-format').text(moment().format("dddd, MMMM Do YYYY, h:mm:ss a"));

}