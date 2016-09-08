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
  $('.bacon').text(bag.Bacon + ' lazy dogs. ' + _.min([4,2,3,6,7]));
}