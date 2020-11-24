1.  Avoid using anonymouse function

Because we are passing anonymous functions, React will always re-render since it receives a new anonymous function as a prop which it is unable to compare to the previous anonymous function (since they are both anonymous)

```jsx
render() { 
    return 
      < button onClick={() => this.handleClick()} /> 
  }
```

2. setState is asynomouse, you could use call back 

Because of its asynchronous nature, setState accepts a second argument that is a function that it invokes after the state has been updated.

```js
handleFirstNameChange = (event) => {
  this.setState({firstName: event.currentTarget.value}, () => {
    console.log(this.state.firstName);
  });
}
```
