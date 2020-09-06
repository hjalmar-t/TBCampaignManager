import React from 'react';
import CharacterSelection from './CharacterSelection.js';
import InventoryManager from './InventoryManager.js';
import ItemInserter from './ItemInserter.js';

const API = 'https://localhost:44349/api/';
const CHAR_QUERY = 'characters/';
const ITEM_QUERY = 'items/';
const INV_QUERY = 'items/ownedBy/';

class Inventory extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      characters: [],
      items: [],
      inventory: [],
      inventoryId: null
    };

    this.selectCharacter = this.selectCharacter.bind(this);
    this.reload = this.reload.bind(this);
  }

  componentDidMount() {
    // get a list of all characters from the database
    fetch(API + CHAR_QUERY)
      .then(response => response.json())
      .then(data => this.setState({characters: data}))
    // get a list of all items from the database
      fetch(API + ITEM_QUERY)
      .then(response => response.json())
      .then(data => this.setState({items: data}))
  }

  async selectCharacter(character) {
    console.log("Selected character: " + character.name + " | Inventory ID: " + character.inventoryId);
    this.setState({inventoryId: character.inventoryId});
    // get a list of specific items from the database
    fetch(API + INV_QUERY + character.inventoryId)
      .then(response => response.json())
      .then(data => this.setState({inventory: data}))
  }

  reload() {
    console.log("rerendered root")
    this.forceUpdate();
  }

  render() {
    if(!this.state.characters[0]) return null;

    return (
      <div className="Wrapper">
        <h1>Tabletop Campaign Manager</h1>

        <CharacterSelection characters={this.state.characters} onSelection={this.selectCharacter}/>
        <InventoryManager inventory={this.state.inventory} inventoryId={this.state.inventoryId} />
        <ItemInserter items={this.state.items} inventoryId={this.state.inventoryId} API={API} onUpdate={this.reload} />
      </div>
      // InventoryRenderer
      /* 
      1. Find all owned items that match selection.InventoryID
      2. Find items that match owned item ID
      */
    );
  }
}

export default Inventory;
