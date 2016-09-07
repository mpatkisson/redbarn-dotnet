function lorem(model) {
  return 'lorem ipsum magna carta ' + model.Foo + ' ' + model.Bar;
}

function bind(model, bag) {
  var loremIpsum = lorem(model),
      fooval = '';
  log('fooval: ' + fooval);
  $('h3').text(model.Foo);
  $('.foo').val(model.Foo + ' and lorem');
  $('#bar').val(model.Bar);
  fooval =  $('.foo').val();
  log('fooval: ' + fooval);
  $('.bar').val(fooval);
  $('.baz').text(model.Baz + ' Mike');
  $('#baz').html('<em>' + loremIpsum + '</em>');
  $('.bacon').text(bag.Bacon + ' lazy dogs.');
}