1.  Avoid using anonymouse function

Because we are passing anonymous functions, React will always re-render since it receives a new anonymous function as a prop which it is unable to compare to the previous anonymous function (since they are both anonymous)

```jsx
render() { 
    return 
      < button onClick={() => this.handleClick()} /> 
  }
```
