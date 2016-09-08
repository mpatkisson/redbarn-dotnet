// What's available
// 
// document       - An AngleSharp IHtmlDocument
// resigQuery     - Analogous to the jQuery object with limited functionality.
// __env_log      - An Action<string> function used for logging from Jint.  This
//                  is aliased to console.log.
// $              - A pure JavaScript function which wraps resigQuery.Select.
//                  This is what you will normally use for model binding.
// _              - The full build of lodash.  Mostly because I could.  This should 
//                  definitely be optional, but it's very useful.
// moment         - Moment.js.  Again, mostly because I could.  This should 
//                  also be optional.

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
  console.log('we got here');
  $('#baz').html('<em>' + loremIpsum + '</em>');
  $('.bacon').text(bag.Bacon + ' lazy dogs. ' + _.min([4, 2, 3, 6, 7]) + ', ' + model.Now);

  $('#lodash-now').text(_.now());
  $('#moment-format').text(moment().format("dddd, MMMM Do YYYY, h:mm:ss a"));
  $('#moment-net-format').text(moment(model.Now).format("DD-MM-YYYY, h:mm:ss a"));

  var items = [
    { foo: 'foo 1', bar: 'bar 1', baz: 'baz 1' },
    { foo: 'foo 2', bar: 'bar 2', baz: 'baz 2' },
    { foo: 'foo 3', bar: 'bar 3', baz: 'baz 3' },
  ];

  $('table > tbody > tr').repeat(model.foos, function (item, row) {
    row.find('.foo').text(item.foo);
    row.find('.bar').text(item.bar);
    row.find('.baz').text(item.baz);
  });

}