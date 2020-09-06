import React from 'react';
import './../_CSS/PageLayout.css';

class ItemRow extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      item: null,
      owned: true
    }

    this.deleteSelected = this.deleteSelected.bind(this);
  }

  static getDerivedStateFromProps(props, state) {
    return {item: props.item, owned: props.owned};
  }

  deleteSelected(id) {
    console.log("deleted item " + id);

    fetch('https://localhost:44349/api/Items/owned/delete/' + id, {method: 'DELETE'});
  }

  render() {
    const item = this.state.item;

    var temp;
    if(this.state.owned) { temp = item.ownedItemId; }
    else { temp = item.itemId; }
    const itemId = temp;
    
    return (
      <tr key={itemId+"r"}>
        <td key={itemId+"d1"}>{item.name}</td>
        <td key={itemId+"d2"}>{item.weight}</td>
        {/* <td key={itemId+"d3"}>{item.count}</td> */}
        <td key={itemId+"del"}> <button onClick={(e) => this.deleteSelected(item.ownedItemId)} >X</button></td>
      </tr>
    );
  }
}

export default ItemRow;
