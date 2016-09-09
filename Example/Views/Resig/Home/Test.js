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

  $('table > tbody > tr').repeat(model.foos, function (item, row) {
    row.find('.foo').text(item.foo);
    row.find('.bar').text(item.bar);
    row.find('.baz').text(item.baz);
  });

  var rows = $(
    '<tr> \
      <td class="foo">keep</td> \
      <td class="bar">keep</td> \
      <td class="baz">keep</td> \
     </tr> \
     <tr> \
      <td class="foo">save</td> \
      <td class="bar">save</td> \
      <td class="baz">save</td> \
     </tr>'
  );

  $('table > tbody')
    .append(rows)
    .append(
      '<tr> \
        <td class="foo">replace</td> \
        <td class="bar">replace</td> \
        <td class="baz">replace</td> \
       </tr>'
     );

  var items = [
    { foo: 'oof 1', bar: 'rab 1', baz: 'zab 1' },
    { foo: 'oof 2', bar: 'rab 2', baz: 'zab 2' },
    { foo: 'oof 3', bar: 'rab 3', baz: 'zab 3' }
  ];

  $('table > tbody > tr:last-child').repeat(items, function (item, row) {
    row.find('.foo').text(item.foo);
    row.find('.bar').text(item.bar);
    row.find('.baz').text(item.baz);
  });

  $('table > tbody').append(function () {
    var now = moment().format('DD-MM-YYYY, h:mm:ss a'),
        row =
          '<tr> \
            <td class="foo">' + now + '</td> \
            <td class="bar">' + now + '</td> \
            <td class="baz">' + now + '</td> \
           </tr>';
    return row;
  });

  //$('table > tbody').append(function () {
  //  var now = moment().format('DD-MM-YYYY, h:mm:ss a'),
  //      row =
  //        '<tr> \
  //          <td class="foo">' + now + '</td> \
  //          <td class="bar">' + now + '</td> \
  //          <td class="baz">' + now + '</td> \
  //         </tr>';
  //  return $(row)[0];
  //});

}