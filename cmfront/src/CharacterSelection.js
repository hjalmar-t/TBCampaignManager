import React from 'react';



class CharacterSelection extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      characters: []
    };
  }

  componentDidMount() {
    fetch('https://localhost:44349/api/characters')
      .then(response => response.json())
      .then(data => this.setState({characters: data}))
  }

  render() {
    if(!this.state.characters[0]) return null;
    var guys = this.state.characters;

    const playerCharacters = guys.map(chars =>
      <li key={chars.CharacterId}>
        {chars.name}, {chars.class}
      </li>
    )


    // for(var i = 0; i <= guys.length; i++) {
    //   elements.push(
    //     <div >
    //       <p key="i"></p>
    //     </div>
    //   )
    // }

    return (
      <div>
        {playerCharacters}
      </div>
    );
  }
}

export default CharacterSelection;
