import React from 'react';

class CharacterSelection1 extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      characters: [],
      selection: ""
    };

    this.selectCharacter = this.selectCharacter.bind(this);
  }

  componentDidMount() {
    fetch('https://localhost:44349/api/characters')
      .then(response => response.json())
      .then(data => this.setState({characters: data}))
  }

  async selectCharacter(name) {
    await this.setState({selection: name});
    console.log("Selected character: " + this.state.selection);
  }

  render() {
    if(!this.state.characters[0]) return null;
    //if(this.state.selection = "none") return null;
    var guys = this.state.characters;

    const playerCharacters = guys.map(chars =>
      <div>
        <button key={chars.CharacterId} onClick={(e) => this.selectCharacter(chars.name)}>{chars.name}, {chars.class}</button>
        <br />
      </div>
    )

    return (
      <div>
        {playerCharacters}
      </div>
    );
  }
}

export default CharacterSelection1;
