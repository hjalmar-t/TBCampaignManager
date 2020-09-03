import React from 'react';
import './../_CSS/PageLayout.css';

class InventoryRow extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      item: null
    }
  }

  static getDerivedStateFromProps(props, state) {
    return {item: props.item};
  }


  render() {
    const item = this.state.item;
    
    return (
      <tr key={item.ownedItemId+"r"}>
        <td key={item.ownedItemId+"d1"}>{item.name}</td>
        <td key={item.ownedItemId+"d2"}>{item.weight}</td>
        <td key={item.ownedItemId+"d3"}>{this.props.count}</td>
        <td key={item.ownedItemId+"del"}><button>X</button></td>
      </tr>
    );
  }
}

//ownedItemId required to handle specific deletions
//can't fully use ownedItem in order to deal with duplicates...

export default InventoryRow;
