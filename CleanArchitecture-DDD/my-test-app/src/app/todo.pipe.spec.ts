import { TodoPipe } from './todo.pipe';

describe('TodoPipe', () => {
  it('create an instance', () => {
    const pipe = new TodoPipe();
    expect(pipe).toBeTruthy();
  });
});
