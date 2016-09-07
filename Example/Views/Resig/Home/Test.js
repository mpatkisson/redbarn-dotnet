function lorem(model) {
  return 'lorem ipsum magna carta ' + model.Foo + ' ' + model.Bar + ' baz';
}

function bind(model, bag) {
  var loremIpsum = lorem(model),
      fooval = '';
  log('fooval: ' + fooval);
  $('.foo').val(model.Foo + ' and lorem');
  $('#bar').val(model.Bar);
  fooval =  $('.foo').val();
  log('fooval: ' + fooval);
  $('.bar').val(fooval);
  $('.baz').text(model.Baz + ' Mike');
  $('#baz').html('<em>' + loremIpsum + '</em>');
  $('.bacon').text(bag.Bacon + ' lazy dogs. ' + _.min([4,2,3,6,7]));
}