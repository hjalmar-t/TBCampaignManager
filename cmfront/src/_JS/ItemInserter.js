import React from 'react';
import './../_CSS/PageLayout.css';

const ITEM_QUERY = "items/owned";

class ItemInserter extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      items: [],
      inventoryId: null,
      API: ""
    }

    this.handleSelection = this.handleSelection.bind(this);
  }

  componentDidMount() {
    // fetch(API + INV_QUERY + this.state.inventoryId)
    // .then(response => response.json())
    // .then(data => this.setState({inventory: data}))
  }

  static getDerivedStateFromProps(props) {
    return {items: props.items, inventoryId: props.inventoryId, API: props.API};
  }

  async handleSelection(item) {
    console.log("selected item: " + item.name + " with inventory id " + this.state.inventoryId);

    const requestOptions = {
      method: 'POST',
      headers: {'Accept': 'application/json', 'Content-Type': 'application/json'},
      body: JSON.stringify(
        {OwnedItemId: 0, InventoryID: this.state.inventoryId, ItemID: item.itemId}
      )
    };
    await fetch(this.state.API + ITEM_QUERY, requestOptions);
    this.props.onUpdate();
  }

  render() { 
    if(this.state.inventoryId === null) return null;

    const itemButtons = this.state.items.map(item =>
      <button key={item.itemId+"ibutt"} onClick={(e) => this.handleSelection(item)}> {item.name} </button>
    );

    return (
      <div className="Wrapper">
        <h3>All items:</h3>
        {itemButtons}
      </div>
    );
  }
}

export default ItemInserter;
