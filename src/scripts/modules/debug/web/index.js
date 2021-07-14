const express = require('express')
const app = express()
const port = 3000

app.use(express.static(__dirname + '/public'));

app.get('/', (req, res) => {
    var content = fs.readFileSync("public/index.html");
    content = content.toString();
    
    res.send(content);
})

app.listen(port, () => {
  console.log(`Example app listening at http://localhost:${port}`)
})