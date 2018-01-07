const text = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc odio mi, venenatis non facilisis quis, ultricies quis velit. Nunc ut justo congue, lobortis neque sed, maximus nulla. Mauris posuere sodales venenatis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nullam a augue scelerisque, tempus erat nec, sodales nisl. Curabitur arcu purus, mattis malesuada bibendum sit amet, elementum vitae libero. Aliquam eu varius diam. Fusce hendrerit, metus in mattis pulvinar, lorem nisi tempus urna, eu pulvinar elit nisl fringilla ex. Etiam maximus faucibus sem, molestie fermentum magna pharetra a. Curabitur tincidunt quam felis, in pharetra urna blandit at. Nullam ut molestie ante.'
const vocabulary = text.replace(/,/g, '').replace(/\./g, '').split(' ')

function getGuid () {
  function s4 () {
    return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1)
  }

  return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4()
}

function getRandomNumber (num) {
  return Math.floor(Math.random() * num)
}

function getDate () {
  let date = new Date()
  date.setFullYear(
        1900 + getRandomNumber(100),
        1 + getRandomNumber(10),
        1 + getRandomNumber(28)
    )

  return date
}

function getWord () {
  let idx = Math.floor(Math.random() * vocabulary.length)
  return vocabulary[idx]
}

function getSentence (wordsCount) {
  let words = []
  for (let i = 0; i < wordsCount; i++) {
    words.push(getWord())
  }

  let str = words.join(' ')
  str = str.charAt(0).toUpperCase() + str
  return str
}

function getHostName () {
  return getWord() + '.com'
}

function getHostNames (num) {
  let hostNames = []
  for (let i = 0; i < num; i++) {
    hostNames.push(getHostName())
  }

  return hostNames
}

function getBlogs () {
  return getHostNames()
}

function getLinks (num) {
  let links = []
  for (let i = 0; i < num; i++) {
    links.push({
      id: getGuid(),
      title: getWord(),
      order: i
    })
  }

  return links
}

function getCategory () {
  return {
    id: getGuid(),
    title: getWord()
  }
}

function getTag () {
  return {
    id: getGuid(),
    title: getWord()
  }
}

function getTags (num) {
  let tags = []
  for (let i = 0; i < num; i++) {
    tags.push(getTag())
  }

  return tags
}

function getPost () {
  return {
    key: getGuid(),
    title: getSentence(3),
    short: getSentence(30 + getRandomNumber(100)),
    body: getSentence(1300 + getRandomNumber(100)),
    date: getDate().toLocaleDateString(),
    category: getCategory(),
    tags: getTags(3)
  }
}

function getPosts (num) {
  let posts = []
  for (let i = 0; i < num; i++) {
    posts.push(getPost())
  }

  return posts
}

const blogs = getBlogs()
const links = getLinks(10)
const posts = getPosts(30)

export {
    blogs,
    links,
    posts
}
